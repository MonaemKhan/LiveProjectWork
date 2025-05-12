use Administrator;
go

create procedure geo.getCountryList
as
begin
select id,country_name from geo.countries order by country_name;
end;

go

create procedure geo.getDivisionListbyCountryId
@countryId int
as
begin
select id,name+' ('+bn_name+')' from geo.divisions where country_id = @countryId order by name
end;

go

create procedure geo.getDivisionList
as
begin
select id,name+' ('+bn_name+')' from geo.divisions order by name
end;

go

create procedure geo.getDistricctListByDivisionId
@divisionId int
as
begin
select id,name+' ('+bn_name+')' from geo.districts where division_id = @divisionId order by name;
end;

go

create procedure geo.getUpazillasListByDistrictId
@districtId int
as
begin
select id,name+' ('+bn_name+')' from geo.upazillas where district_id = @districtId order by name;
end;

go

create procedure geo.getUnionListByUpazillaId
@upazillaId int
as
begin
select id,name+' ('+bn_name+')' from geo.unions where upazilla_id = @upazillaId order by name;
end;