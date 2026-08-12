package com.requestflow.controller;

import com.requestflow.dto.AssigneeUpdate;
import com.requestflow.dto.HistoryEntryDto;
import com.requestflow.dto.RequestCreate;
import com.requestflow.dto.RequestDto;
import com.requestflow.dto.StatusUpdate;
import com.requestflow.model.RequestStatus;
import com.requestflow.service.RequestService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/requests")
@RequiredArgsConstructor
public class RequestController {

    private final RequestService requestService;

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public RequestDto create(@Valid @RequestBody RequestCreate request) {
        return requestService.create(request);
    }

    @GetMapping
    public List<RequestDto> list(@RequestParam(required = false) RequestStatus status) {
        return requestService.list(status);
    }

    @GetMapping("/{id}")
    public RequestDto getById(@PathVariable Long id) {
        return requestService.getById(id);
    }

    @PatchMapping("/{id}/assignee")
    public RequestDto assign(@PathVariable Long id, @Valid @RequestBody AssigneeUpdate request) {
        return requestService.assign(id, request);
    }

    @PatchMapping("/{id}/status")
    public RequestDto updateStatus(@PathVariable Long id, @Valid @RequestBody StatusUpdate request) {
        return requestService.updateStatus(id, request);
    }

    @GetMapping("/{id}/history")
    public List<HistoryEntryDto> getHistory(@PathVariable Long id) {
        return requestService.getHistory(id);
    }
}
