-- Add realmDB updates for upcoming login system updates.
ALTER TABLE `accounts`
ADD COLUMN `banned`  int(1) NULL DEFAULT 0 AFTER `password`;

