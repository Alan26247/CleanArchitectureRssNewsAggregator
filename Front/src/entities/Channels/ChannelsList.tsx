import { ChannelItem } from "./ChannelItem";

export class ChannelsList {
    data: ChannelItem[] = new Array<ChannelItem>();
    pageNumber: number = 1;
    pageSize: number = 20;
    pageCount: number = 0;
    countItems: number = 0;
}