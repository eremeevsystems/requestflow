package com.requestflow.service;

import com.requestflow.dto.LoginRequest;
import com.requestflow.dto.LoginResponse;
import com.requestflow.model.Role;
import com.requestflow.model.User;
import com.requestflow.repository.UserRepository;
import com.requestflow.security.JwtService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Optional;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class AuthServiceTest {

    @Mock
    private UserRepository userRepository;
    @Mock
    private PasswordEncoder passwordEncoder;
    @Mock
    private JwtService jwtService;

    @InjectMocks
    private AuthService authService;

    @Test
    void login_returnsTokenAndUserOnValidCredentials() {
        User user = user(1L, "manager1", Role.MANAGER);
        when(userRepository.findByUsername("manager1")).thenReturn(Optional.of(user));
        when(passwordEncoder.matches("secret", "hash")).thenReturn(true);
        when(jwtService.generateToken("manager1", "MANAGER")).thenReturn("jwt-token");

        LoginResponse response = authService.login(new LoginRequest("manager1", "secret"));

        assertThat(response.token()).isEqualTo("jwt-token");
        assertThat(response.user().username()).isEqualTo("manager1");
        assertThat(response.user().role()).isEqualTo(Role.MANAGER);
    }

    @Test
    void login_throwsOnUnknownUser() {
        when(userRepository.findByUsername("nobody")).thenReturn(Optional.empty());

        assertThatThrownBy(() -> authService.login(new LoginRequest("nobody", "secret")))
                .isInstanceOf(BadCredentialsException.class);
    }

    @Test
    void login_throwsOnWrongPassword() {
        User user = user(1L, "manager1", Role.MANAGER);
        when(userRepository.findByUsername("manager1")).thenReturn(Optional.of(user));
        when(passwordEncoder.matches("wrong", "hash")).thenReturn(false);

        assertThatThrownBy(() -> authService.login(new LoginRequest("manager1", "wrong")))
                .isInstanceOf(BadCredentialsException.class);
    }

    private static User user(Long id, String username, Role role) {
        User user = new User();
        user.setId(id);
        user.setUsername(username);
        user.setPasswordHash("hash");
        user.setRole(role);
        return user;
    }
}
