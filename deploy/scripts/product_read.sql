create table product_read (
    id UUID ,
    page String(1024),
    production String(512),
    category String(512),
    producer String(512),
    username String(512),
    created DateTime
    )
ENGINE= MergeTree()
--ORDER BY (id, str)
PRIMARY KEY (id)