import { httpCore } from 'shared/configs/instance'

// ----- добавление канала -----
export interface ICreateChannel {
  title: string,
  description: string,
  rssLink: string,
}

export const createChannel = (data: ICreateChannel) => httpCore.post('api/channel', {
  title: data.title,
  description: data.description,
  rssLink: data.rssLink
});

// ----- обновление канала -----
export interface IUpdateChannel {
  id: number,
  title: string,
  description: string,
}

export const updateChannel = (data: IUpdateChannel) => httpCore.put('api/channel', {
  id: data.id,
  title: data.title,
  description: data.description,
});

// ----- удаление канала -----
export interface IDeleteChannel {
  id: number,
}

export const deleteChannel = (data: IDeleteChannel) => httpCore.delete(`api/channel/${data.id}`);

// ----- получение списка каналов -----
export interface IGetChannelList {
  findString: string | null,
  pageNumber: number,
  pageSize: number,
}

export const getChannelList = (data: IGetChannelList) => httpCore.get('api/channel/list', {
  params: {
    findString: data.findString,
    pageNumber: data.pageNumber,
    pageSize: data.pageSize
  }
});

// ----- обновление списка каналов -----
export const updateChannelsNews = () => httpCore.put('api/channel/list/update-news');