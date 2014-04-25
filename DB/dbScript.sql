drop table if exists Subscription;
drop table if exists Cupon;
drop table if exists Ad;
drop table if exists Manager_Institution_maps;
drop table if exists Institution;
drop table if exists Inst_Group;
drop table if exists Manager;
drop table if exists Client;
drop table if exists Subscribable;
drop table if exists Account;

--create table Account (
--	id int not null auto_increment primary key,
--	type enum('client', 'institution'),
--	fb_id bigint not null,
--	receive_notification boolean default 1,
--	show_ads boolean default 1,
--	currency enum('USD', 'EUR', 'YEN')
--);

CREATE TABLE Account (
  id int NOT NULL AUTO_INCREMENT,
  type enum('client','institution') DEFAULT 'client',
  fb_id bigint(20) NOT NULL,
  receive_notification boolean DEFAULT 1,
  show_ads boolean DEFAULT 1,
  currency enum('USD','EUR','YEN') DEFAULT 'EUR',
  PRIMARY KEY (id),
  UNIQUE KEY `fb_id_UNIQUE` (fb_id)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8

create table Subscribable (
	id int not null auto_increment primary key
);

--create table Client (
--	id int not null primary key,
--	name varchar(255) not null,
--	address varchar(255) not null,
--	city varchar(50) not null,
--	phone_number varchar(50) not null,
--	nif varchar(50) not null,
--	foreign key (id) references Acccount(id) on delete cascade
--);

CREATE TABLE Client (
  id int(11) NOT NULL,
  name varchar(255) NOT NULL,
  address varchar(255) DEFAULT NULL,
  city varchar(50) NOT NULL,
  phone_number varchar(50) DEFAULT NULL,
  nif varchar(50) DEFAULT NULL,
  email varchar(50) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=MyISAM DEFAULT CHARSET=utf8

create table Manager (
	id int not null primary key,
	username varchar(50) not null,
	password varchar(255) not null,
	foreign key (id) references Acccount(id) on delete cascade
);

create table Inst_Group (
	id int not null auto_increment primary key,
	name varchar(255) not null,
	website varchar(255) not null
);

create table Institution (
	id int not null primary key,
	group_id int,
	name varchar(255) not null,
	address varchar(255) not null,
	city varchar(50) not null,
	latitude decimal(10, 8) not null,
	longitude decimal(11, 8) not null,
	email varchar(255) not null,
	website varchar(255) not null,
	phone_number varchar(50) not null,
	fax varchar(50),
	foreign key (id) references Subscribable(id) on delete cascade,
	foreign key (group_id) references Inst_Group(id) on delete cascade
);

create table Manager_Institution_maps (
	manager_id int,
	institution_id int,
	foreign key (manager_id) references Manager(id) on delete cascade,
	foreign key (institution_id) references Institution(id) on delete cascade,
	primary key (manager_id, institution_id)
);

create table Ad (
	id int not null primary key,
	institution_id int not null,
	service varchar(255) not null,
	specialty varchar(255) not null,
	name varchar(255) not null,
	description varchar(255) not null,
	price decimal(6,2) not null,
	discount float,
	start_time datetime not null,
	end_time datetime not null,
	remaining_cupons int not null,
	buyed_cupons int not null,
	foreign key (id) references Subscribable(id) on delete cascade,
	foreign key (institution_id) references Institution(id) on delete cascade
);

create table Cupon (
	id int not null auto_increment primary key,
	client_id int not null,
	ad_id int not null,
	state int not null,
	--start_time datetime not null,
	start_time date not null,
	--end_time datetime not null,
	end_time date not null,
	--purchase_time datetime not null,
	purchase_time date not null,
	foreign key (ad_id) references Ad(id) on delete cascade,
	foreign key (client_id) references Client(id) on delete cascade
);

create table Subscription (
	subscribable_id int not null,
	client_id int not null,
	foreign key (subscribable_id) references Subscribable(id) on delete cascade,
	foreign key (client_id) references Client(id) on delete cascade,
	primary key (subscribable_id, client_id)
);

