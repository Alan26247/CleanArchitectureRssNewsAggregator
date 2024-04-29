import { useState } from "react";
import styles from './SelectorNumberUI.module.css';

interface ISelectorNumberUIProps {
    values: number[],
    onChange: (value: number) => void
}

const SelectorNumberUI = ({ values, onChange }: ISelectorNumberUIProps) => {

    const [selectedNumber, setSelectedNumber] = useState<number>(values[0]);

    const selectNumber = (value: number) => {
        if (selectedNumber === value) return;
        setSelectedNumber(value);
        onChange(value);
    }

    return (
        <div className={styles.content}>
            {values.map((value) => (
                <span
                    key={value}
                    className={`${styles.item} ${selectedNumber === value ? styles.is_selected : ''}`}
                    onClick={() => selectNumber(value)}>
                    {value}
                </span>
            ))}
        </div>
    );
}

export default SelectorNumberUI;