package com.requestflow.dto;

import com.requestflow.model.RequestPriority;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record RequestCreate(
                @NotBlank @Size(max = 255) String title,
                String description,
                RequestPriority priority) {
}
