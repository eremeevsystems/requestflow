CREATE TABLE users (
    id            BIGSERIAL PRIMARY KEY,
    username      VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role          VARCHAR(20)  NOT NULL CHECK (role IN ('MANAGER', 'EXECUTOR'))
);

CREATE TABLE request (
    id          BIGSERIAL PRIMARY KEY,
    title       VARCHAR(255) NOT NULL,
    description TEXT,
    priority    VARCHAR(20)  NOT NULL DEFAULT 'MEDIUM'
                             CHECK (priority IN ('LOW', 'MEDIUM', 'HIGH')),
    status      VARCHAR(20)  NOT NULL DEFAULT 'NEW'
                             CHECK (status IN ('NEW', 'ASSIGNED', 'IN_PROGRESS', 'COMPLETED')),
    created_by  BIGINT       NOT NULL REFERENCES users (id),
    assigned_to BIGINT       REFERENCES users (id),
    created_at  TIMESTAMPTZ  NOT NULL DEFAULT now(),
    updated_at  TIMESTAMPTZ  NOT NULL DEFAULT now()
);

CREATE INDEX idx_request_status ON request (status);
CREATE INDEX idx_request_assigned_to ON request (assigned_to);

CREATE TABLE request_history (
    id         BIGSERIAL PRIMARY KEY,
    request_id BIGINT      NOT NULL REFERENCES request (id) ON DELETE CASCADE,
    changed_by BIGINT      NOT NULL REFERENCES users (id),
    old_status VARCHAR(20) CHECK (old_status IN ('NEW', 'ASSIGNED', 'IN_PROGRESS', 'COMPLETED')),
    new_status VARCHAR(20) NOT NULL CHECK (new_status IN ('NEW', 'ASSIGNED', 'IN_PROGRESS', 'COMPLETED')),
    comment    TEXT,
    changed_at TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE INDEX idx_request_history_request_id ON request_history (request_id);
