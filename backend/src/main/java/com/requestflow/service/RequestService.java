package com.requestflow.service;

import com.requestflow.dto.AssigneeUpdate;
import com.requestflow.dto.HistoryEntryDto;
import com.requestflow.dto.RequestCreate;
import com.requestflow.dto.RequestDto;
import com.requestflow.dto.StatusUpdate;
import com.requestflow.exception.BadRequestException;
import com.requestflow.exception.ForbiddenException;
import com.requestflow.exception.NotFoundException;
import com.requestflow.model.Request;
import com.requestflow.model.RequestHistory;
import com.requestflow.model.RequestStatus;
import com.requestflow.model.Role;
import com.requestflow.model.User;
import com.requestflow.repository.RequestHistoryRepository;
import com.requestflow.repository.RequestRepository;
import com.requestflow.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Map;
import java.util.Set;

@Service
@RequiredArgsConstructor
public class RequestService {

    private static final Map<RequestStatus, Set<RequestStatus>> ALLOWED_TRANSITIONS = Map.of(
            RequestStatus.ASSIGNED, Set.of(RequestStatus.IN_PROGRESS),
            RequestStatus.IN_PROGRESS, Set.of(RequestStatus.COMPLETED));

    private final RequestRepository requestRepository;
    private final RequestHistoryRepository historyRepository;
    private final UserRepository userRepository;

    @Transactional
    public RequestDto create(RequestCreate dto) {
        User currentUser = currentUser();
        Request request = new Request();
        request.setTitle(dto.title());
        request.setDescription(dto.description());
        if (dto.priority() != null) {
            request.setPriority(dto.priority());
        }
        request.setStatus(RequestStatus.NEW);
        request.setCreatedBy(currentUser);
        Request saved = requestRepository.save(request);
        addHistory(saved, currentUser, null, RequestStatus.NEW, "Request created");
        return toDto(saved);
    }

    @Transactional(readOnly = true)
    public List<RequestDto> list(RequestStatus status) {
        User currentUser = currentUser();
        List<Request> requests;
        if (currentUser.getRole() == Role.MANAGER) {
            requests = status == null ? requestRepository.findAll() : requestRepository.findByStatus(status);
        } else {
            requests = status == null
                    ? requestRepository.findByAssignedToId(currentUser.getId())
                    : requestRepository.findByAssignedToIdAndStatus(currentUser.getId(), status);
        }
        return requests.stream().map(this::toDto).toList();
    }

    @Transactional(readOnly = true)
    public RequestDto getById(Long id) {
        return toDto(findRequest(id));
    }

    @Transactional
    public RequestDto assign(Long id, AssigneeUpdate dto) {
        User currentUser = currentUser();
        if (currentUser.getRole() != Role.MANAGER) {
            throw new ForbiddenException("Only MANAGER can assign executors");
        }
        Request request = findRequest(id);
        User assignee = userRepository.findById(dto.assigneeId())
                .orElseThrow(() -> new NotFoundException("User not found: " + dto.assigneeId()));
        if (assignee.getRole() != Role.EXECUTOR) {
            throw new BadRequestException("Assignee must have role EXECUTOR");
        }
        if (request.getStatus() != RequestStatus.NEW) {
            throw new BadRequestException("Cannot assign request in status " + request.getStatus());
        }
        request.setAssignedTo(assignee);
        request.setStatus(RequestStatus.ASSIGNED);
        addHistory(request, currentUser, RequestStatus.NEW, RequestStatus.ASSIGNED,
                "Assigned to " + assignee.getUsername());
        return toDto(requestRepository.save(request));
    }

    @Transactional
    public RequestDto updateStatus(Long id, StatusUpdate dto) {
        User currentUser = currentUser();
        Request request = findRequest(id);
        if (request.getAssignedTo() == null || !request.getAssignedTo().getId().equals(currentUser.getId())) {
            throw new ForbiddenException("Only the assigned executor can change the status");
        }
        RequestStatus oldStatus = request.getStatus();
        RequestStatus newStatus = dto.status();
        if (!ALLOWED_TRANSITIONS.getOrDefault(oldStatus, Set.of()).contains(newStatus)) {
            throw new BadRequestException("Invalid status transition: " + oldStatus + " -> " + newStatus);
        }
        request.setStatus(newStatus);
        addHistory(request, currentUser, oldStatus, newStatus, null);
        return toDto(requestRepository.save(request));
    }

    @Transactional(readOnly = true)
    public List<HistoryEntryDto> getHistory(Long id) {
        User currentUser = currentUser();
        Request request = findRequest(id);
        boolean allowed = currentUser.getRole() == Role.MANAGER
                || request.getCreatedBy().getId().equals(currentUser.getId())
                || (request.getAssignedTo() != null && request.getAssignedTo().getId().equals(currentUser.getId()));
        if (!allowed) {
            throw new ForbiddenException("No access to this request's history");
        }
        return historyRepository.findByRequestIdOrderByChangedAtDesc(id).stream()
                .map(this::toHistoryDto)
                .toList();
    }

    private Request findRequest(Long id) {
        return requestRepository.findById(id)
                .orElseThrow(() -> new NotFoundException("Request not found: " + id));
    }

    private User currentUser() {
        String username = SecurityContextHolder.getContext().getAuthentication().getName();
        return userRepository.findByUsername(username)
                .orElseThrow(() -> new NotFoundException("User not found: " + username));
    }

    private void addHistory(Request request, User changedBy, RequestStatus oldStatus,
            RequestStatus newStatus, String comment) {
        RequestHistory entry = new RequestHistory();
        entry.setRequest(request);
        entry.setChangedBy(changedBy);
        entry.setOldStatus(oldStatus);
        entry.setNewStatus(newStatus);
        entry.setComment(comment);
        historyRepository.save(entry);
    }

    private RequestDto toDto(Request request) {
        return new RequestDto(
                request.getId(),
                request.getTitle(),
                request.getDescription(),
                request.getPriority(),
                request.getStatus(),
                AuthService.toDto(request.getCreatedBy()),
                request.getAssignedTo() == null ? null : AuthService.toDto(request.getAssignedTo()),
                request.getCreatedAt(),
                request.getUpdatedAt());
    }

    private HistoryEntryDto toHistoryDto(RequestHistory entry) {
        return new HistoryEntryDto(
                entry.getId(),
                entry.getRequest().getId(),
                AuthService.toDto(entry.getChangedBy()),
                entry.getOldStatus(),
                entry.getNewStatus(),
                entry.getComment(),
                entry.getChangedAt());
    }
}
