import type { RootState } from 'app/store';
import { useSelector } from 'react-redux';
import styles from './SpinnerUI.module.css';

const SpinnerUI = () => {

    const spinner = useSelector((state: RootState) => state.spinner);

    return (
        <>
            {spinner.isVisible && (
                <div className={styles.space}>
                    <div className="spinner-border text-primary"
                        style={{ width: '240px', height: '240px' }}
                        role="status"
                    >
                        <span className="visually-hidden">Loading...</span>
                    </div>
                </div>
            )}
        </>
    );
}

export default SpinnerUI;