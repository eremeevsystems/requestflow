package com.requestflow.repository;

import com.requestflow.model.Request;
import com.requestflow.model.RequestStatus;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface RequestRepository extends JpaRepository<Request, Long> {

    List<Request> findByStatus(RequestStatus status);

    List<Request> findByAssignedToId(Long assignedToId);

    List<Request> findByAssignedToIdAndStatus(Long assignedToId, RequestStatus status);
}
