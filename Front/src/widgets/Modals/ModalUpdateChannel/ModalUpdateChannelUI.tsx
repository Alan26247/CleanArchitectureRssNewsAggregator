import { useState } from "react";
import ButtonSmallUI from 'widgets/Components/ButtonSmall/ButtonSmallUI';
import styles from './ModalUpdateChannelUI.module.css';
import { updateChannel } from 'shared/api/channel';
import { useDispatch } from 'react-redux';
import { outInfo } from 'features/info/infoSlice';
import { ChannelItem } from "entities/Channels/ChannelItem";

interface IModalUpdateChannelUIProps {
    channel: ChannelItem,
    onClose: () => void,
    onEdit: () => void,
}

const ModalUpdateChannelUI = ({ channel, onClose, onEdit }: IModalUpdateChannelUIProps) => {

    const dispatch = useDispatch();

    const [title, setChannelTitle] = useState(channel.title);
    const [description, setChannelDescription] = useState(channel.description);

    const updateCurrentChannel = () => {
        updateChannel({
            id: channel.id,
            title: title,
            description: description,
        }).then((res) => {
            console.log('успешно', res);
            dispatch(outInfo({
                isSuccess: true,
                text: 'Канал успешно отредактирован',
            }));
            onClose();
            onEdit();
            return;
        }).catch((err) => {
            console.log('ошибка', err);
            dispatch(outInfo({
                isSuccess: false,
                text: 'Ошибка редактирования канала',
            }));
            onClose();
        })
    }

    return (
        <>
            <div className={styles.space}>
                <div className={`card ${styles.window}`}>
                    <div className={`card-body ${styles.content}`}>
                        <h5 className={styles.title}>Редактирование канала</h5>
                        <div className="input-group">
                            <span className="input-group-text" id="basic-addon1">Название канала</span>
                            <input type="text" className="form-control" value={title} onChange={(e) => setChannelTitle(e.target.value)} placeholder='Название' aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group">
                            <span className="input-group-text" id="basic-addon1">Описание канала</span>
                            <input type="text" className="form-control" value={description} onChange={(e) => setChannelDescription(e.target.value)} placeholder='Описание' aria-describedby="basic-addon1" />
                        </div>
                        <div className={styles.buttons}>
                            <ButtonSmallUI text="отмена" width={120} onClick={onClose} />
                            <ButtonSmallUI text="редактировать" width={120} onClick={updateCurrentChannel} />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default ModalUpdateChannelUI;