CREATE VIEW [dbo].[Employee]
	AS SELECT [ID],
			  [UserName],
			  [Email],
			  [PhoneNumber],
			  [FirstName],
			  [LastName],
			  [AddressLine],
			  [PostalCode],
			  [Town]			  
			  FROM [ApplicationUser]
