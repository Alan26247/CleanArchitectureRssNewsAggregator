import { httpCore } from 'shared/configs/instance'

export const getNewsList = (data: { 
  findString: string | null,
  channelId: number | null,
  pageNumber: number,
  pageSize: number,
}) =>  httpCore.get('api/news/list', {
  params: {
    findString: data.findString,
    channelId: data.channelId,
    pageNumber: data.pageNumber,
    pageSize: data.pageSize
  }
});