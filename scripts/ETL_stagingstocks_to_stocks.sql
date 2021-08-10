USE [StockScreener]
GO




insert into Exchanges([symbol], [name])
select ss.exch, ss.exchDisp from 
(select exch, exchDisp
from stagingstocks 
group by exch, exchDisp
) as ss
left outer join Exchanges as e on e.Symbol = ss.exch
where e.Symbol is null 
go

insert into SecurityTypes ([type], [name])
select ss.type, ss.typeDisp from 
(select type, typeDisp
from stagingstocks 
group by type, typeDisp
) as ss
left outer join SecurityTypes as st on st.type = ss.type
where st.type is null 

insert into Stocks ( Symbol, Name, ExchangeSymbol, SecurityTypeType)
--select¡@symbol, count(*)
--from (
	select ss2.symbol, ss2.name, ss2.exch, ss2.[type]
	from (
	select symbol, max(id) as id 
	from StagingStocks
	group by symbol
	) as ss1
	inner join stagingstocks as ss2 on ss1.id = ss2.Id
	left outer join Stocks as s on s.Symbol = ss2.symbol
	where s.Symbol is null
--) as o group by o.symbol
--having count(*) > 1
