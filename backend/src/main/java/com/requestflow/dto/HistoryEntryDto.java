package com.requestflow.dto;

import com.requestflow.model.RequestStatus;

import java.time.OffsetDateTime;

public record HistoryEntryDto(
                Long id,
                Long requestId,
                UserDto changedBy,
                RequestStatus oldStatus,
                RequestStatus newStatus,
                String comment,
                OffsetDateTime changedAt) {
}
