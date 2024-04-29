import { useEffect, useState } from "react";
import ChannelUI from "./Components/ChannelItemUI";
import styles from './ChannelListUI.module.css';
import { ChannelItem } from "entities/Channels/ChannelItem";
import { ChannelsList } from "entities/Channels/ChannelsList";
import PaginationUI from "widgets/Components/Pagination/PaginationUI";
import FindStringUI from "widgets/Components/FindString/FindStringUI";
import { getChannelList } from "shared/api/channel";
import SelectorNumberUI from "widgets/Components/SelectorNumber/SelectorNumberUI";
import ButtonSmallUI from "widgets/Components/ButtonSmall/ButtonSmallUI";
import { updateChannelsNews } from 'shared/api/channel';
import { useDispatch } from 'react-redux';
import { changeChannel } from 'features/channel/channelSlice';
import { outInfo } from 'features/info/infoSlice';
import { setSpinner } from 'features/spinner/spinnerSlice';
import ModalAddChannelUI from "widgets/Modals/ModaAddChannel/ModalAddChannelUI";
import ModalDeleteChannelUI from "widgets/Modals/ModalDeleteChannel/ModalDeleteChannelUI";
import ModalUpdateChannelUI from "widgets/Modals/ModalUpdateChannel/ModalUpdateChannelUI";


const ChannelListUI = () => {

    const dispatch = useDispatch();

    const [channels, setChannels] = useState<ChannelsList | null>(null);
    const [currentChannelId, setCurrentChannelId] = useState<number | null>(null);
    const [currentChannelFindString, setCurrentChannelFindString] = useState<string>('');
    const [currentChannelPageNumber, setCurrentChannelPageNumber] = useState<number>(1);
    const [currentChannelPageSize, setCurrentChannelPageSize] = useState<number>(5);

    const [openWindowCreateChannel, setOpenWindowCreateChannel] = useState<boolean>(false);


    useEffect(() => {
        updateChannels(currentChannelFindString, currentChannelPageNumber, currentChannelPageSize);
    }, []);

    const updateChannels = (
        channelFindString: string,
        channelPageNumber: number,
        channelPageSize: number) => {
        getChannelList({
            findString: channelFindString,
            pageNumber: channelPageNumber,
            pageSize: channelPageSize,
        }).then((res) => {
            setChannels(res.data.data);
            return;
        }
        )
    }

    const changeChannelsFindString = (value: string) => {
        updateChannels(value, 1, currentChannelPageSize);
        setCurrentChannelFindString(value);
        setCurrentChannelPageNumber(1);
    }

    const changeChannelPage = (pageNumber: number) => {
        updateChannels(currentChannelFindString, pageNumber, currentChannelPageSize);
        setCurrentChannelPageNumber(pageNumber);
    }

    const changeCurrentChannel = (value: ChannelItem | null) => {
        setCurrentChannelId(value ? value.id : null);
        dispatch(changeChannel({
            channelId: value ? value.id : null,
            channelTitle: value ? value.title : "Все каналы",}));
    }

    const changeChannelPageSize = (value: number) => {
        updateChannels(currentChannelFindString, currentChannelPageNumber, value);
        setCurrentChannelPageSize(value);
    }

    const changeOpenWindowCreateChannel = () => {
        setOpenWindowCreateChannel(!openWindowCreateChannel);
    }

    // ----- обновление канала -----
    const [openWindowUpdateChannel, setOpenWindowUpdateChannel] = useState<boolean>(false);
    const [channelForUpdate, setChannelForUpdate] = useState<ChannelItem | null>(null);

    const updateChannel = (channel: ChannelItem | null) => {
        if (channel === null) return;
        setChannelForUpdate(channel);
        setOpenWindowUpdateChannel(true);
    }

    const closeOpenWindowUpdateChannel = () => {
        setOpenWindowUpdateChannel(false);
    }

    const onUpdateChannel = () => {
        updateChannels(currentChannelFindString, currentChannelPageNumber, currentChannelPageSize);
    }

    // ----- удаление канала -----
    const [openWindowDeleteChannel, setOpenWindowDeleteChannel] = useState<boolean>(false);
    const [channelForDelete, setChannelForDelete] = useState<ChannelItem | null>(null);

    const deleteChannel = (channel: ChannelItem | null) => {
        if (channel === null) return;
        setChannelForDelete(channel);
        setOpenWindowDeleteChannel(true);
    }

    const closeOpenWindowDeleteChannel = () => {
        setOpenWindowDeleteChannel(false);
    }

    const onDeleteChannel = () => {
        updateChannels(currentChannelFindString, currentChannelPageNumber, currentChannelPageSize);
    }

    // ----- обновление всех каналов -----
    const updateAllChannels = () => {
        dispatch(setSpinner(true));
        const activeElement = document.activeElement as HTMLElement | null;
        if (activeElement) {
            activeElement.blur();
        }
        updateChannelsNews().then((res) => {
            dispatch(outInfo({
                isSuccess: true,
                text: 'Каналы успешно обновлены',
            }));
            dispatch(setSpinner(false));
            return;
        }
        )
    }

    return (
        <>
            <div className={styles.container}>
                <div className={styles.title}>
                    <h3 className={styles.title_text}>Каналы</h3>
                    <SelectorNumberUI values={[5, 10, 15]} onChange={changeChannelPageSize} />
                    <ButtonSmallUI text="+" width={40} onClick={changeOpenWindowCreateChannel} />
                    {/* <ButtonSmallUI text="Обновить каналы" onClick={updateAllChannels} /> */}
                </div>
                <FindStringUI onChange={changeChannelsFindString} />
                <ChannelUI
                    channel={null}
                    isSelected={currentChannelId == null}
                    onClick={changeCurrentChannel}
                    onDelete={deleteChannel}
                    onUpdate={updateChannel}
                />
                {channels && (
                    <div className={styles.container}>
                        {channels.data.map((item: ChannelItem) => (
                            <ChannelUI
                                key={item.id}
                                channel={item}
                                isSelected={currentChannelId === item.id}
                                onClick={changeCurrentChannel}
                                onDelete={deleteChannel}
                                onUpdate={updateChannel}
                            />
                        ))}
                        <PaginationUI
                            pageNumber={currentChannelPageNumber}
                            countPages={channels.pageCount}
                            updatePage={changeChannelPage}
                        />
                    </div>
                )}
            </div>
            {openWindowCreateChannel && <ModalAddChannelUI onClose={changeOpenWindowCreateChannel} onAdd={onUpdateChannel} />}
            {openWindowUpdateChannel && channelForUpdate &&
                <ModalUpdateChannelUI
                    channel={channelForUpdate}
                    onClose={closeOpenWindowUpdateChannel}
                    onEdit={onUpdateChannel} />
            }
            {openWindowDeleteChannel && channelForDelete &&
                <ModalDeleteChannelUI
                    channel={channelForDelete}
                    onClose={closeOpenWindowDeleteChannel}
                    onDelete={onDeleteChannel} />
            }
        </>
    );
}

export default ChannelListUI;