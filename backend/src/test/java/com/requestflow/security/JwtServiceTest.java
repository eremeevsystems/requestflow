package com.requestflow.security;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

class JwtServiceTest {

    private static final String SECRET = "requestflow-dev-secret-key-0123456789abcdef0123456789abcdef";

    private final JwtService jwtService = new JwtService(SECRET, 86400000L);

    @Test
    void generateToken_extractsUsernameRoundtrip() {
        String token = jwtService.generateToken("manager1", "MANAGER");

        assertThat(jwtService.extractUsername(token)).isEqualTo("manager1");
        assertThat(jwtService.isValid(token)).isTrue();
    }

    @Test
    void isValid_returnsFalseForGarbage() {
        assertThat(jwtService.isValid("not-a-token")).isFalse();
    }

    @Test
    void isValid_returnsFalseForTokenSignedWithAnotherKey() {
        JwtService otherService = new JwtService(
                "another-secret-key-0123456789abcdef0123456789abcdef0123456789", 86400000L);
        String token = otherService.generateToken("manager1", "MANAGER");

        assertThat(jwtService.isValid(token)).isFalse();
    }

    @Test
    void isValid_returnsFalseForExpiredToken() {
        JwtService expiredService = new JwtService(SECRET, -1000L);
        String token = expiredService.generateToken("manager1", "MANAGER");

        assertThat(jwtService.isValid(token)).isFalse();
    }
}
