import { NewsItem } from "./NewsItem";

export class NewsList {
    data: NewsItem[] = new Array<NewsItem>();
    pageNumber: number = 1;
    pageSize: number = 20;
    pageCount: number = 0;
    countItems: number = 0;
}