package com.requestflow.dto;

import com.requestflow.model.RequestStatus;
import jakarta.validation.constraints.NotNull;

public record StatusUpdate(
                @NotNull RequestStatus status) {
}
