package com.requestflow.dto;

import jakarta.validation.constraints.NotNull;

public record AssigneeUpdate(
                @NotNull Long assigneeId) {
}
