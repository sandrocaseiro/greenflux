CREATE TABLE "group" (
	id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	name TEXT(100) NOT NULL,
	capacity NUMERIC NOT NULL
);

CREATE TABLE charge_station (
	id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	name TEXT(100) NOT NULL,
	group_id INTEGER NOT NULL,
	CONSTRAINT charge_station_FK FOREIGN KEY (group_id) REFERENCES "group"(id)
);

CREATE TABLE connector (
	id INTEGER NOT NULL,
	max_current NUMERIC NOT NULL,
	charge_station_id INTEGER NOT NULL,
	CONSTRAINT connector_PK PRIMARY KEY (id,charge_station_id),
	CONSTRAINT connector_FK FOREIGN KEY (charge_station_id) REFERENCES charge_station(id),
	CONSTRAINT connector_UN UNIQUE (id,charge_station_id)
);
