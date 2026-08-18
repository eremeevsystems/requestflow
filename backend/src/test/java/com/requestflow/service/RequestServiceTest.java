package com.requestflow.service;

import com.requestflow.dto.AssigneeUpdate;
import com.requestflow.dto.HistoryEntryDto;
import com.requestflow.dto.RequestCreate;
import com.requestflow.dto.RequestDto;
import com.requestflow.dto.StatusUpdate;
import com.requestflow.exception.BadRequestException;
import com.requestflow.exception.ForbiddenException;
import com.requestflow.exception.NotFoundException;
import com.requestflow.model.Request;
import com.requestflow.model.RequestPriority;
import com.requestflow.model.RequestStatus;
import com.requestflow.model.Role;
import com.requestflow.model.User;
import com.requestflow.repository.RequestHistoryRepository;
import com.requestflow.repository.RequestRepository;
import com.requestflow.repository.UserRepository;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.ArgumentCaptor;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;

import java.util.List;
import java.util.Optional;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.lenient;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class RequestServiceTest {

    @Mock
    private RequestRepository requestRepository;
    @Mock
    private RequestHistoryRepository historyRepository;
    @Mock
    private UserRepository userRepository;

    @InjectMocks
    private RequestService requestService;

    @AfterEach
    void clearSecurityContext() {
        SecurityContextHolder.clearContext();
    }

    @Test
    void create_savesRequestWithStatusNew() {
        User manager = authenticate(user(1L, "manager1", Role.MANAGER));
        when(requestRepository.save(any(Request.class))).thenAnswer(inv -> inv.getArgument(0));

        RequestDto dto = requestService.create(new RequestCreate("Fix printer", "Broken", null));

        assertThat(dto.status()).isEqualTo(RequestStatus.NEW);
        assertThat(dto.title()).isEqualTo("Fix printer");
        assertThat(dto.priority()).isEqualTo(RequestPriority.MEDIUM);
        ArgumentCaptor<Request> captor = ArgumentCaptor.forClass(Request.class);
        verify(requestRepository).save(captor.capture());
        assertThat(captor.getValue().getCreatedBy()).isEqualTo(manager);
        verify(historyRepository).save(any());
    }

    @Test
    void list_managerSeesAllRequests() {
        authenticate(user(1L, "manager1", Role.MANAGER));
        when(requestRepository.findAll()).thenReturn(List.of());

        requestService.list(null);

        verify(requestRepository).findAll();
        verify(requestRepository, never()).findByAssignedToId(any());
    }

    @Test
    void list_executorSeesOnlyAssignedRequests() {
        authenticate(user(2L, "executor1", Role.EXECUTOR));
        when(requestRepository.findByAssignedToIdAndStatus(2L, RequestStatus.IN_PROGRESS))
                .thenReturn(List.of());

        requestService.list(RequestStatus.IN_PROGRESS);

        verify(requestRepository).findByAssignedToIdAndStatus(2L, RequestStatus.IN_PROGRESS);
        verify(requestRepository, never()).findByStatus(any());
    }

    @Test
    void assign_managerAssignsExecutor() {
        authenticate(user(1L, "manager1", Role.MANAGER));
        User executor = user(2L, "executor1", Role.EXECUTOR);
        Request request = request(10L, RequestStatus.NEW);
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request));
        when(userRepository.findById(2L)).thenReturn(Optional.of(executor));
        when(requestRepository.save(any(Request.class))).thenAnswer(inv -> inv.getArgument(0));

        RequestDto dto = requestService.assign(10L, new AssigneeUpdate(2L));

        assertThat(dto.status()).isEqualTo(RequestStatus.ASSIGNED);
        assertThat(dto.assignedTo().username()).isEqualTo("executor1");
    }

    @Test
    void assign_executorIsForbidden() {
        authenticate(user(2L, "executor1", Role.EXECUTOR));

        assertThatThrownBy(() -> requestService.assign(10L, new AssigneeUpdate(2L)))
                .isInstanceOf(ForbiddenException.class);
        verify(requestRepository, never()).save(any());
    }

    @Test
    void assign_rejectsNonExecutorAssignee() {
        authenticate(user(1L, "manager1", Role.MANAGER));
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request(10L, RequestStatus.NEW)));
        when(userRepository.findById(3L)).thenReturn(Optional.of(user(3L, "manager2", Role.MANAGER)));

        assertThatThrownBy(() -> requestService.assign(10L, new AssigneeUpdate(3L)))
                .isInstanceOf(BadRequestException.class);
    }

    @Test
    void assign_rejectsRequestNotInNewStatus() {
        authenticate(user(1L, "manager1", Role.MANAGER));
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request(10L, RequestStatus.ASSIGNED)));
        when(userRepository.findById(2L)).thenReturn(Optional.of(user(2L, "executor1", Role.EXECUTOR)));

        assertThatThrownBy(() -> requestService.assign(10L, new AssigneeUpdate(2L)))
                .isInstanceOf(BadRequestException.class);
    }

    @Test
    void updateStatus_assignedExecutorCanFollowAllowedTransitions() {
        User executor = authenticate(user(2L, "executor1", Role.EXECUTOR));
        Request request = request(10L, RequestStatus.ASSIGNED);
        request.setAssignedTo(executor);
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request));
        when(requestRepository.save(any(Request.class))).thenAnswer(inv -> inv.getArgument(0));

        RequestDto dto = requestService.updateStatus(10L, new StatusUpdate(RequestStatus.IN_PROGRESS));

        assertThat(dto.status()).isEqualTo(RequestStatus.IN_PROGRESS);
    }

    @Test
    void updateStatus_rejectsInvalidTransition() {
        User executor = authenticate(user(2L, "executor1", Role.EXECUTOR));
        Request request = request(10L, RequestStatus.ASSIGNED);
        request.setAssignedTo(executor);
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request));

        assertThatThrownBy(() -> requestService.updateStatus(10L, new StatusUpdate(RequestStatus.COMPLETED)))
                .isInstanceOf(BadRequestException.class);
    }

    @Test
    void updateStatus_forbidsNonAssignedUser() {
        authenticate(user(3L, "executor2", Role.EXECUTOR));
        Request request = request(10L, RequestStatus.ASSIGNED);
        request.setAssignedTo(user(2L, "executor1", Role.EXECUTOR));
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request));

        assertThatThrownBy(() -> requestService.updateStatus(10L, new StatusUpdate(RequestStatus.IN_PROGRESS)))
                .isInstanceOf(ForbiddenException.class);
    }

    @Test
    void getById_throwsWhenRequestMissing() {
        when(requestRepository.findById(99L)).thenReturn(Optional.empty());

        assertThatThrownBy(() -> requestService.getById(99L))
                .isInstanceOf(NotFoundException.class);
    }

    @Test
    void getHistory_forbidsUserWithoutAccess() {
        authenticate(user(3L, "executor2", Role.EXECUTOR));
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request(10L, RequestStatus.NEW)));

        assertThatThrownBy(() -> requestService.getHistory(10L))
                .isInstanceOf(ForbiddenException.class);
    }

    @Test
    void getHistory_allowsCreator() {
        User creator = authenticate(user(1L, "manager1", Role.MANAGER));
        Request request = request(10L, RequestStatus.NEW);
        request.setCreatedBy(creator);
        when(requestRepository.findById(10L)).thenReturn(Optional.of(request));
        when(historyRepository.findByRequestIdOrderByChangedAtDesc(10L)).thenReturn(List.of());

        List<HistoryEntryDto> history = requestService.getHistory(10L);

        assertThat(history).isEmpty();
        verify(historyRepository).findByRequestIdOrderByChangedAtDesc(10L);
    }

    private User authenticate(User user) {
        SecurityContextHolder.getContext().setAuthentication(
                new UsernamePasswordAuthenticationToken(user.getUsername(), null, List.of()));
        lenient().when(userRepository.findByUsername(user.getUsername())).thenReturn(Optional.of(user));
        return user;
    }

    private static User user(Long id, String username, Role role) {
        User user = new User();
        user.setId(id);
        user.setUsername(username);
        user.setRole(role);
        return user;
    }

    private static Request request(Long id, RequestStatus status) {
        Request request = new Request();
        request.setId(id);
        request.setTitle("Request " + id);
        request.setStatus(status);
        request.setCreatedBy(user(1L, "manager1", Role.MANAGER));
        return request;
    }
}
