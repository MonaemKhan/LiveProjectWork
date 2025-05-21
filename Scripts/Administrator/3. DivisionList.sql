use Administrator;
go
CREATE TABLE geo.divisions (
  id INT primary key NOT NULL,
  name nvarchar(100) NOT NULL,
  country_id INT not null,
  bn_name nvarchar(100) NOT NULL,
  url nvarchar(100) NOT NULL,
  CONSTRAINT FK_Divisions_Country FOREIGN KEY (country_id)
        REFERENCES geo.countries(Id)
        ON DELETE no action
        ON UPDATE no action
);
go
INSERT INTO geo.divisions (id, name, bn_name, url,country_id) VALUES
(1, 'Chattagram', N'চট্টগ্রাম', 'www.chittagongdiv.gov.bd',14),
(2, 'Rajshahi', N'রাজশাহী', 'www.rajshahidiv.gov.bd',14),
(3, 'Khulna', N'খুলনা', 'www.khulnadiv.gov.bd',14),
(4, 'Barisal', N'বরিশাল', 'www.barisaldiv.gov.bd',14),
(5, 'Sylhet', N'সিলেট', 'www.sylhetdiv.gov.bd',14),
(6, 'Dhaka', N'ঢাকা', 'www.dhakadiv.gov.bd',14),
(7, 'Rangpur', N'রংপুর', 'www.rangpurdiv.gov.bd',14),
(8, 'Mymensingh', N'ময়মনসিংহ', 'www.mymensinghdiv.gov.bd',14);
