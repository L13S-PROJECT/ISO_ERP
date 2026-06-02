INSERT INTO Categories (Id, Name, IsActive)
SELECT
    ID,
    category_name,
    IsActive
FROM erp_fmm.categories;

INSERT INTO Products (Id, Name, Code, CategoryId, IsActive)
SELECT
    ID,
    product_name,
    product_code,
    category_id,
    IsActive
FROM erp_fmm.products;