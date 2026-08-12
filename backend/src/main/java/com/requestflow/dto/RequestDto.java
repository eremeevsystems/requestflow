package com.requestflow.dto;

import com.requestflow.model.RequestPriority;
import com.requestflow.model.RequestStatus;

import java.time.OffsetDateTime;

public record RequestDto(
                Long id,
                String title,
                String description,
                RequestPriority priority,
                RequestStatus status,
                UserDto createdBy,
                UserDto assignedTo,
                OffsetDateTime createdAt,
                OffsetDateTime updatedAt) {
}
