interface IButtonSmallUIProps {
    text: string,
    width?: number,
    height?: number,
    onClick: () => void
}

const ButtonSmallUI = ({ text, width, height, onClick }: IButtonSmallUIProps) => {
    return (
        <>
            <button
                type="button"
                className="btn btn-primary btn-sm"
                style={{ width: width !== null ? `${width}px` : 'auto', height: height !== null ? `${height}px` : '40px'}}
                onClick={onClick}
            >
                {text}
            </button>
        </>
    );
}

export default ButtonSmallUI;