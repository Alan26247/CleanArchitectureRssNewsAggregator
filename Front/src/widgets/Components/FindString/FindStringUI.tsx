const FindStringUI = ({ onChange }: { onChange:(value: string) => void }) => {
    return (
        <div className="input-group">
            <input
                type="text"
                className="form-control"
                placeholder=""
                aria-label="Recipient's username"
                aria-describedby="basic-addon2"
                onChange={(e) => onChange(e.target.value)}
            />
            <span className="input-group-text" id="basic-addon2">поиск</span>
        </div>
    );
}

export default FindStringUI;