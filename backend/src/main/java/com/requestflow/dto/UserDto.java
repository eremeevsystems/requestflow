package com.requestflow.dto;

import com.requestflow.model.Role;

public record UserDto(
                Long id,
                String username,
                Role role) {
}
