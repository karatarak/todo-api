CREATE TABLE IF NOT EXISTS users (
    user_id UUID PRIMARY KEY,
	user_name VARCHAR(64) NOT NULL UNIQUE,
    created_date TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS boards (
    board_id UUID PRIMARY KEY,
    owner_id UUID NOT NULL,
	title VARCHAR(100) NOT NULL,
	created_date TIMESTAMPTZ NOT NULL,
	FOREIGN KEY (owner_id) REFERENCES users (user_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS items (
    item_id UUID PRIMARY KEY,
    owner_id UUID NOT NULL,
	board_id UUID NOT NULL,
	title VARCHAR(100) NOT NULL,
	description VARCHAR,
	status VARCHAR(64) NOT NULL,
	created_date TIMESTAMPTZ NOT NULL,
	due_date TIMESTAMPTZ,
	FOREIGN KEY (owner_id) REFERENCES users (user_id) ON DELETE CASCADE,
	FOREIGN KEY (board_id) REFERENCES boards (board_id) ON DELETE CASCADE
);