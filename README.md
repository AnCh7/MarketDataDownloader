Market data downloader for IQFeed and Fidelity data feeds.
Program can download ticks, intraday and EOD data.

Output format:

1) Fidelity
* TimeStamp, Open, High, Low, Open, Close, Volume

2) IQFeed
* Tick data: TimeStamp, Last, LastSize, TotalVolume, Bid, Ask, TickId, BasisForLast, TradeMarketCenter, TradeConditions
* Intraday data: TimeStamp, Open, High, Low, Close, TotalVolume, PeriodVolume
* EOD: TimeStamp, Open, High, Low, Close, PeriodVolume

For "Real-time updating" functionality please use default datetime format.

You can adjust settings in the config file:
1) UpdateInterval - updating interval for "Real-time updating" functionality (in milliseconds).
2) UpdateIntervalTick - same, for ticks only (in milliseconds).
3) Symbols - list of tickers with space as delimiter.
4) FolderForSaving
5) AmountOfDays

IQFeed specification:
* 120 calendar days of tick (includes pre-post market)
* Several years of 1-Minute history (Forex back to Feb 2005, Eminis back to Sept. 2005, Stock/Futures/Indexes back to May 2007)
* Daily, Weekly and Monthly Historical data (15+ years of O,H,L,C,V,OI data)

Full description here: http://www.iqfeed.net/index.cfm?displayaction=data&section=services

Market symbols you can find here
* http://www.iqfeed.net/symbolguide/index.cfm?symbolguide=lookup&displayaction=support&section=guide&web=iqfeed
* http://www.iqfeed.net/symbolguide/index.cfm?symbolguide=guide&displayaction=support&section=guide&web=iqfeed