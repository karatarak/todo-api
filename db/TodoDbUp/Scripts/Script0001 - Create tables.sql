CREATE TABLE IF NOT EXISTS `user` (
    `user_id` BINARY(16) PRIMARY KEY,
	`user_name` VARCHAR(64) NOT NULL,
    `created_date` DATETIME NOT NULL,
    UNIQUE KEY (`user_name`)
);

CREATE TABLE IF NOT EXISTS `board` (
    `board_id` BINARY(16) PRIMARY KEY,
    `owner_id` BINARY(16) NOT NULL,
	`title` VARCHAR(100) NOT NULL,
	`created_date` DATETIME NOT NULL,
	FOREIGN KEY (`owner_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS `item` (
    `item_id` BINARY(16) PRIMARY KEY,
    `owner_id` BINARY(16) NOT NULL,
	`board_id` BINARY(16) NOT NULL,
	`status` VARCHAR(64) NOT NULL,
	`title` VARCHAR(100) NOT NULL,
	`description` TEXT,
	`created_date` DATETIME NOT NULL,
	`due_date` DATETIME,
	FOREIGN KEY (`owner_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE,
	FOREIGN KEY (`board_id`) REFERENCES `board` (`board_id`) ON DELETE CASCADE
);