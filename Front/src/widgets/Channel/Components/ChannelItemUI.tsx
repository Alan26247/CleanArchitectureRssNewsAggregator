import { ChannelItem } from 'entities/Channels/ChannelItem';
import styles from './ChannelItemUI.module.css';

interface IChannelItemProps {
    channel: ChannelItem | null,
    isSelected: boolean,
    onClick: (value: ChannelItem | null) => void,
    onDelete: (value: ChannelItem | null) => void,
    onUpdate: (value: ChannelItem | null) => void,
}

const ChannelItemUI = ({ channel, isSelected, onClick, onDelete, onUpdate }: IChannelItemProps) => {
    return (
        <div className={`card w-100 ${isSelected ? 'bg-primary text-white' : ''}`}
        >
            <div className={`${styles.content} card-body text-start`} onClick={() => onClick(channel)}>
                <div className={styles.title_container}>
                    <h5 className="card-title">{channel ? channel.title : 'Все каналы'}</h5>
                    {channel && !channel.isFixed &&
                        <div className={styles.buttons}>
                            <div className={styles.button}  onClick={() => onUpdate(channel)}>
                                <img src='images/buttons/edit.png' className={styles.image}  alt='update'/>
                            </div>
                            <div className={styles.button}  onClick={() => onDelete(channel)} >
                                <img src='images/buttons/delete.png' className={styles.image}alt='delete' />
                            </div>
                        </div>
                    }
                </div>
                <p className="card-text">{channel ? channel.description : 'Список всех каналов'}</p>
            </div>
        </div>
    );
}

export default ChannelItemUI;