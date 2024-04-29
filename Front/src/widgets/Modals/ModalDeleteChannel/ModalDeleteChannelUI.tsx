import ButtonSmallUI from 'widgets/Components/ButtonSmall/ButtonSmallUI';
import styles from './ModalDeleteChannelUI.module.css';
import { deleteChannel } from 'shared/api/channel';
import { useDispatch } from 'react-redux'
import { outInfo } from 'features/info/infoSlice'
import { ChannelItem } from "entities/Channels/ChannelItem";

interface IModalCreateChannelUIProps {
    channel: ChannelItem,
    onClose: () => void,
    onDelete: () => void,
}

const ModalDeleteChannelUI = ({ channel, onClose, onDelete }: IModalCreateChannelUIProps) => {

    const dispatch = useDispatch();

    const deleteCurrentChannel = (id: number) => {
        deleteChannel({
            id: id,
        }).then((res) => {
            dispatch(outInfo({
                isSuccess: true,
                text: 'Канал успешно удален',
            }));
            onClose();
            onDelete();
            return;
        }).catch((err) => {
            dispatch(outInfo({
                isSuccess: false,
                text: 'Ошибка при удалении канала',
            }));
            onClose();
        })
    }

    return (
        <>
            <div className={styles.space}>
                <div className={`card ${styles.window}`}>
                    <div className={`card-body ${styles.content}`}>
                        <h5 className={styles.title}>Удаление канала</h5>
                        <p>Вы уверены, что хотите удалить канал - {channel.title}?</p>
                        <div className={styles.buttons}>
                            <ButtonSmallUI text="отмена" width={120} onClick={onClose} />
                            <ButtonSmallUI text="удалить канал" width={120} onClick={() =>deleteCurrentChannel(channel.id)} />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default ModalDeleteChannelUI;