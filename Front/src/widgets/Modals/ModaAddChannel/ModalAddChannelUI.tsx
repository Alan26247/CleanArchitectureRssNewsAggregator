import { useState } from "react";
import ButtonSmallUI from 'widgets/Components/ButtonSmall/ButtonSmallUI';
import styles from './ModalAddChannelUI.module.css';
import { createChannel } from 'shared/api/channel';
import { useDispatch } from 'react-redux';
import { outInfo } from 'features/info/infoSlice';
import { setSpinner } from 'features/spinner/spinnerSlice';

interface IModalCreateChannelUIProps {
    onClose: () => void,
    onAdd: () => void,
}

const ModalCreateChannelUI = ({ onClose, onAdd }: IModalCreateChannelUIProps) => {

    const dispatch = useDispatch();

    const [title, setChannelTitle] = useState('');
    const [description, setChannelDescription] = useState('');
    const [rssLink, setChannelRssLink] = useState('');

    const addChannel = () => {
        dispatch(setSpinner(true));
        createChannel({
            title: title,
            description: description,
            rssLink: rssLink,
        }).then((res) => {
            console.log('успешно', res);
            dispatch(outInfo({
                isSuccess: true,
                text: 'Канал успешно добавлен',
            }));
            dispatch(setSpinner(false));
            onAdd();
            onClose();
            return;
        }).catch((err) => {
            console.log('ошибка', err);
            dispatch(outInfo({
                isSuccess: false,
                text: 'URL на валиден либо имеет ограничение по доступу',
            }));
            dispatch(setSpinner(false));
            onClose();
        })
    }

    return (
        <>
            <div className={styles.space}>
                <div className={`card ${styles.window}`}>
                    <div className={`card-body ${styles.content}`}>
                        <h5 className={styles.title}>Добавление нового канала</h5>
                        <div className="input-group">
                            <span className="input-group-text" id="basic-addon1">Название канала</span>
                            <input type="text" className="form-control" value={title} onChange={(e) => setChannelTitle(e.target.value)} placeholder='Название' aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group">
                            <span className="input-group-text" id="basic-addon1">Описание канала</span>
                            <input type="text" className="form-control" value={description} onChange={(e) => setChannelDescription(e.target.value)} placeholder='Описание' aria-describedby="basic-addon1" />
                        </div>
                        <div className="input-group">
                            <span className="input-group-text" id="basic-addon1">URL канала</span>
                            <input type="text" className="form-control" value={rssLink} onChange={(e) => setChannelRssLink(e.target.value)} placeholder='URL канала' aria-describedby="basic-addon1" />
                        </div>
                        <div className={styles.buttons}>
                            <ButtonSmallUI text="отмена" width={120} onClick={onClose} />
                            <ButtonSmallUI text="создать канал" width={120} onClick={addChannel} />
                        </div>
                        <hr />
                        <h5>Образцы URL RSS каналов для создания и тестирования</h5>
                        <span>https://habr.com/ru/rss/articles/top/?fl=ru</span>
                        <span>https://news.rambler.ru/rss/Stavropol</span>
                        <span>https://www.finam.ru/international/imdaily/rsspoint</span>
                        <span>https://www.interfax.ru/rss.asp</span>
                        <hr />
                    </div>
                </div>
            </div>
        </>
    );
}

export default ModalCreateChannelUI;