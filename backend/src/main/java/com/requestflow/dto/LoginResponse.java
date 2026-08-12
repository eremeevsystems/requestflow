package com.requestflow.dto;

public record LoginResponse(
                String token,
                UserDto user) {
}
