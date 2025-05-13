namespace CentralModels.Administration
{
    public class UserDetailsView
    {
        public int id { get; set; }
        public string? user_id { get; set; }
        public string? user_name { get; set; }
        public string? user_password { get; set; }
        public string? user_email { get; set; }
        public string? user_phone { get; set; }
        public string? user_dob { get; set; }
        public string? user_nid { get; set; }
        public int? user_county_id { get; set; }
        public int? user_division_id { get; set; }
        public int? user_district_id { get; set; }
        public int? user_upazilla_id { get; set; }
        public int? user_union_id { get; set; }
        public int? user_active { get; set; }
        public int? user_allow_access_any_office { get; set; }
        public string? STATUS { get; set; }
        public string? make_by { get; set; }
        public string? make_dt { get; set; }
        public string? last_modify_by { get; set; }
        public string? last_modify_dt { get; set; }
        public string? project_id { get; set; }
        public string? user_type { get; set; }
        public string? country_name { get; set; }
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? upozilla_name { get; set; }
        public string? union_name { get; set; }
    }
}
