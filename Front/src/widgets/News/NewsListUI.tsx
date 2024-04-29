import { useEffect, useState } from "react";
import styles from './NewsListUI.module.css';
import NewsItemUI from "widgets/News/Components/NewsItemUI";
import { NewsItem } from "entities/News/NewsItem";
import { NewsList } from "entities/News/NewsList";
import PaginationUI from 'widgets/Components/Pagination/PaginationUI';
import FindStringUI from "widgets/Components/FindString/FindStringUI";
import { getNewsList } from "shared/api/news";
import SelectorNumberUI from "widgets/Components/SelectorNumber/SelectorNumberUI";
import type { RootState } from 'app/store';
import { useSelector } from 'react-redux';

const NewsListUI = () => {

    const channel = useSelector((state: RootState) => state.channel);

    const [news, setNews] = useState<NewsList | null>(null);
    const [newsFindString, setNewsFindString] = useState<string>('');
    const [newsPageNumber, setNewsPageNumber] = useState<number>(1);
    const [newsPageSize, setNewsPageSize] = useState<number>(5);

    useEffect(() => {
        updateNews(newsFindString, channel.channelId, 1, newsPageSize);
        setNewsPageNumber(1);
    }, [channel.channelId]);

    const updateNews = (
        newsFindString: string,
        channelId: number | null,
        newsPageNumber: number,
        newsPageSize: number
    ) => {
        getNewsList({
            findString: newsFindString,
            channelId: channelId,
            pageNumber: newsPageNumber,
            pageSize: newsPageSize,
        }).then((res) => {
            setNews(res.data.data);
            return;
        }
        )
    }

    const changePage = (pageNumber: number) => {
        updateNews(newsFindString, channel.channelId, pageNumber, newsPageSize);
        setNewsPageNumber(pageNumber);
        document.body.scrollTop = 0; // Для Safari
        document.documentElement.scrollTop = 0; // Для Chrome, Firefox, IE и Opera
    }

    const changeFindString = (value: string) => {
        updateNews(value, channel.channelId, newsPageNumber, newsPageSize);
        setNewsFindString(value);
    }


    const changeChannelPageSize = (value: number) => {
        updateNews(newsFindString, channel.channelId, 1, value);
        setNewsPageSize(value);
        setNewsPageNumber(1);
    }

    return (
        <>
            {news && (
                <div className={styles.container}>
                    <div className={styles.title}>
                        <h3 className={styles.title_text}>Новости</h3>
                        <SelectorNumberUI values={[5, 10, 25, 50, 100]} onChange={changeChannelPageSize} />
                    </div>
                    <h3 className={styles.channel_title}>{channel.channelTitle}</h3>
                    <FindStringUI onChange={changeFindString} />
                    {news?.data && news?.data.length > 0 && (
                        <>
                            <PaginationUI pageNumber={news?.pageNumber ?? 1} countPages={news?.pageCount ?? 5} updatePage={changePage} />
                            {news.data.map((item: NewsItem) => (
                                <NewsItemUI key={item.id} data={item} isEnableChannelName={true} />
                            ))}
                            <PaginationUI pageNumber={news?.pageNumber ?? 1} countPages={news?.pageCount ?? 5} updatePage={changePage} />
                        </>
                    )}
                </div>
            )}
        </>
    );
}

export default NewsListUI;