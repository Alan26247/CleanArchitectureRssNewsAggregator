import ButtonSmallUI from 'widgets/Components/ButtonSmall/ButtonSmallUI';
import styles from './ModalInfoUI.module.css';
import { useDispatch } from 'react-redux';
import { closeInfo } from 'features/info/infoSlice';

interface IModalInfoUIProps {
    isOpen: boolean,
    description: string,
    isSuccess: boolean,
}

const ModalInfoUI = ({ isOpen, description, isSuccess }: IModalInfoUIProps) => {

    const dispatch = useDispatch();

    const close = () => {
        dispatch(closeInfo());
    }

    return (
        <>
            {isOpen && (
                <div className={styles.space}>
                    <div className={`card ${styles.window}`}>
                        <div className={`card-body ${styles.content} ${isSuccess ? styles.is_success : styles.is_error}`}>
                            <h5 className={styles.title}>{isSuccess ? 'УСПЕШНО' : 'ОШИБКА'}</h5>
                            <hr />
                            <p style={{textAlign: 'left'}}>{description}</p>
                            <hr />
                            <div className={styles.buttons}>
                                <ButtonSmallUI text="ок" width={200} onClick={(close)} />
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
}

export default ModalInfoUI;