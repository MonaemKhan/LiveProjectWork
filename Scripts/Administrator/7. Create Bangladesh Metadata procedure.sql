use Administrator;
go

create procedure admin.getCountryList
as
begin
select id,country_name from geo.countries order by country_name;
end;

go

create procedure admin.getDivisionListbyCountryId
@countryId int
as
begin
select id,name+' ('+bn_name+')' from geo.divisions where country_id = @countryId order by name
end;

go

create procedure admin.getDivisionList
as
begin
select id,name+' ('+bn_name+')' from geo.divisions order by name
end;

go

create procedure admin.getDistricctListByDivisionId
@divisionId int
as
begin
select id,name+' ('+bn_name+')' from geo.districts where division_id = @divisionId order by name;
end;

go

create procedure admin.getUpazilasListByDistrictId
@districtId int
as
begin
select id,name+' ('+bn_name+')' from geo.upazilas where district_id = @districtId order by name;
end;

go

create procedure admin.getUnionListByUpazillaId
@upazillaId int
as
begin
select id,name+' ('+bn_name+')' from geo.unions where upazilla_id = @upazillaId order by name;
end;