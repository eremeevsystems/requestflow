package com.requestflow;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.requestflow.model.Role;
import com.requestflow.model.User;
import com.requestflow.repository.RequestHistoryRepository;
import com.requestflow.repository.RequestRepository;
import com.requestflow.repository.UserRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.test.context.DynamicPropertyRegistry;
import org.springframework.test.context.DynamicPropertySource;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.testcontainers.containers.PostgreSQLContainer;
import org.testcontainers.junit.jupiter.Container;
import org.testcontainers.junit.jupiter.Testcontainers;

import static org.assertj.core.api.Assertions.assertThat;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.patch;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
@Testcontainers
class RequestFlowApiIntegrationTest {

    private static final String PASSWORD = "secret";

    @Container
    static PostgreSQLContainer<?> postgres = new PostgreSQLContainer<>("postgres:16-alpine");

    @DynamicPropertySource
    static void datasourceProperties(DynamicPropertyRegistry registry) {
        registry.add("spring.datasource.url", postgres::getJdbcUrl);
        registry.add("spring.datasource.username", postgres::getUsername);
        registry.add("spring.datasource.password", postgres::getPassword);
    }

    @Autowired
    private MockMvc mockMvc;
    @Autowired
    private ObjectMapper objectMapper;
    @Autowired
    private UserRepository userRepository;
    @Autowired
    private RequestRepository requestRepository;
    @Autowired
    private RequestHistoryRepository historyRepository;
    @Autowired
    private PasswordEncoder passwordEncoder;

    @BeforeEach
    void setUp() {
        historyRepository.deleteAll();
        requestRepository.deleteAll();
        userRepository.deleteAll();
        createUser("manager1", Role.MANAGER);
        createUser("executor1", Role.EXECUTOR);
        createUser("executor2", Role.EXECUTOR);
    }

    @Test
    void requestsRequireAuthentication() throws Exception {
        mockMvc.perform(get("/requests"))
                .andExpect(status().isUnauthorized());
    }

    @Test
    void loginRejectsWrongPassword() throws Exception {
        mockMvc.perform(post("/auth/login")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"username\": \"manager1\", \"password\": \"wrong\"}"))
                .andExpect(status().isUnauthorized());
    }

    @Test
    void createRequestValidatesTitle() throws Exception {
        String managerToken = login("manager1");

        mockMvc.perform(post("/requests")
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"title\": \"\"}"))
                .andExpect(status().isBadRequest());
    }

    @Test
    void fullRequestLifecycle() throws Exception {
        String managerToken = login("manager1");
        String executorToken = login("executor1");
        long executorId = userId("executor1");

        MvcResult created = mockMvc.perform(post("/requests")
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"title\": \"Fix printer\", \"description\": \"Broken\", \"priority\": \"HIGH\"}"))
                .andExpect(status().isCreated())
                .andExpect(jsonPath("$.status").value("NEW"))
                .andReturn();
        long requestId = objectMapper.readTree(created.getResponse().getContentAsString()).get("id").asLong();

        mockMvc.perform(patch("/requests/{id}/assignee", requestId)
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"assigneeId\": " + executorId + "}"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("ASSIGNED"))
                .andExpect(jsonPath("$.assignedTo.username").value("executor1"));

        mockMvc.perform(patch("/requests/{id}/status", requestId)
                        .header("Authorization", bearer(executorToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"status\": \"IN_PROGRESS\"}"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("IN_PROGRESS"));

        mockMvc.perform(patch("/requests/{id}/status", requestId)
                        .header("Authorization", bearer(executorToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"status\": \"COMPLETED\"}"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("COMPLETED"));

        MvcResult history = mockMvc.perform(get("/requests/{id}/history", requestId)
                        .header("Authorization", bearer(managerToken)))
                .andExpect(status().isOk())
                .andReturn();
        JsonNode entries = objectMapper.readTree(history.getResponse().getContentAsString());
        assertThat(entries).hasSize(4);
        assertThat(entries.get(0).get("newStatus").asText()).isEqualTo("COMPLETED");
    }

    @Test
    void executorCannotAssignRequests() throws Exception {
        String managerToken = login("manager1");
        String executorToken = login("executor1");

        MvcResult created = mockMvc.perform(post("/requests")
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"title\": \"Fix printer\"}"))
                .andExpect(status().isCreated())
                .andReturn();
        long requestId = objectMapper.readTree(created.getResponse().getContentAsString()).get("id").asLong();

        mockMvc.perform(patch("/requests/{id}/assignee", requestId)
                        .header("Authorization", bearer(executorToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"assigneeId\": " + userId("executor1") + "}"))
                .andExpect(status().isForbidden());
    }

    @Test
    void executorSeesOnlyAssignedRequests() throws Exception {
        String managerToken = login("manager1");
        String executorToken = login("executor1");

        long firstId = createRequest(managerToken, "First");
        createRequest(managerToken, "Second");

        mockMvc.perform(patch("/requests/{id}/assignee", firstId)
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"assigneeId\": " + userId("executor1") + "}"))
                .andExpect(status().isOk());

        mockMvc.perform(get("/requests").header("Authorization", bearer(executorToken)))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.length()").value(1));

        mockMvc.perform(get("/requests").header("Authorization", bearer(managerToken)))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.length()").value(2));
    }

    private void createUser(String username, Role role) {
        User user = new User();
        user.setUsername(username);
        user.setPasswordHash(passwordEncoder.encode(PASSWORD));
        user.setRole(role);
        userRepository.save(user);
    }

    private long userId(String username) {
        return userRepository.findByUsername(username).orElseThrow().getId();
    }

    private String login(String username) throws Exception {
        MvcResult result = mockMvc.perform(post("/auth/login")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"username\": \"" + username + "\", \"password\": \"" + PASSWORD + "\"}"))
                .andExpect(status().isOk())
                .andReturn();
        return objectMapper.readTree(result.getResponse().getContentAsString()).get("token").asText();
    }

    private long createRequest(String managerToken, String title) throws Exception {
        MvcResult result = mockMvc.perform(post("/requests")
                        .header("Authorization", bearer(managerToken))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\"title\": \"" + title + "\"}"))
                .andExpect(status().isCreated())
                .andReturn();
        return objectMapper.readTree(result.getResponse().getContentAsString()).get("id").asLong();
    }

    private static String bearer(String token) {
        return "Bearer " + token;
    }
}
