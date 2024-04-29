import { NewsItem } from "entities/News/NewsItem";
import styles from "./NewsItemUI.module.css";

interface INewsItemUIProps {
    data: NewsItem,
    isEnableChannelName: boolean,
}

const NewsItemUI = ({ data, isEnableChannelName }: INewsItemUIProps) => {
    const datetime = new Date(data.pubDate);
    const localTimeString = datetime.toLocaleString("ru-RU", {
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
    });
    const [date, time] = localTimeString.split(", ");

    return (
        <div className="card w-100">
            <div className={`card-body text-start ${styles.content}`}>
                {isEnableChannelName && <span className={styles.channel_title}>{data.channelTitle}</span>}
                <h5 className="card-title">{data.title}</h5>
                <div dangerouslySetInnerHTML={{ __html: data.description }} className={`card-text ${styles.content}`} />
                <div className="d-flex justify-content-end align-items-center w-100">
                    <span>{`${time}\n${date}`}</span>
                </div>
                <div className="d-flex justify-content-center align-items-center w-100">
                    <a
                        className="link-offset-2 link-underline link-underline-opacity-25"
                        href={data.link}
                        target="_blank"
                        rel="noreferrer"
                    >
                        перейти
                    </a>
                </div>
            </div>
        </div>
    );
}

export default NewsItemUI;