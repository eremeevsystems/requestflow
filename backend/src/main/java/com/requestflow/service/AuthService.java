package com.requestflow.service;

import com.requestflow.dto.LoginRequest;
import com.requestflow.dto.LoginResponse;
import com.requestflow.dto.UserDto;
import com.requestflow.model.User;
import com.requestflow.repository.UserRepository;
import com.requestflow.security.JwtService;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final JwtService jwtService;

    @Transactional(readOnly = true)
    public LoginResponse login(LoginRequest request) {
        User user = userRepository.findByUsername(request.username())
                .orElseThrow(() -> new BadCredentialsException("Invalid username or password"));
        if (!passwordEncoder.matches(request.password(), user.getPasswordHash())) {
            throw new BadCredentialsException("Invalid username or password");
        }
        String token = jwtService.generateToken(user.getUsername(), user.getRole().name());
        return new LoginResponse(token, toDto(user));
    }

    static UserDto toDto(User user) {
        return new UserDto(user.getId(), user.getUsername(), user.getRole());
    }
}
