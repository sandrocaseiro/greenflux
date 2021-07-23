insert into "group" (name, capacity) values ('group1', 10); --ID: 1
insert into "group" (name, capacity) values ('group2', 5); --ID: 2
insert into "group" (name, capacity) values ('group3', 4); --ID: 3
insert into "group" (name, capacity) values ('group4', 5.5); --ID: 4
insert into "group" (name, capacity) values ('group5', 10); --ID: 5

insert into charge_station (name, group_id) values ('station1.1', 1); --ID: 1
insert into charge_station (name, group_id) values ('station1.2', 1); --ID: 2

insert into charge_station (name, group_id) values ('station2.1', 2); --ID: 3

insert into charge_station (name, group_id) values ('station3.1', 3); --ID: 4
insert into charge_station (name, group_id) values ('station3.2', 3); --ID: 5

insert into charge_station (name, group_id) values ('station4.1', 4); --ID: 6

insert into connector (id, max_current, charge_station_id) values (1, 2, 1);
insert into connector (id, max_current, charge_station_id) values (2, 5, 1);
insert into connector (id, max_current, charge_station_id) values (3, 1, 1);

insert into connector (id, max_current, charge_station_id) values (1, 1, 2);

insert into connector (id, max_current, charge_station_id) values (1, 1, 3);
insert into connector (id, max_current, charge_station_id) values (2, 2, 3);
insert into connector (id, max_current, charge_station_id) values (3, 2, 3);

insert into connector (id, max_current, charge_station_id) values (1, 1, 4);
insert into connector (id, max_current, charge_station_id) values (2, 2, 4);
