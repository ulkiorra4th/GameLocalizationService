interface PageSizeSelectorProps {
    value: number;
    onChange: (value: number) => void;
}

export default function PageSizeSelector({ value, onChange }: PageSizeSelectorProps) {
    const selectClass = "px-2 border border-neutral-400 rounded-lg focus:ring hover:border-neutral-300 " +
        "focus:ring-neutral-400 bg-neutral-900 text-white appearance-none";

    return (
        <span className="flex items-center gap-2">
            <span className="text-white">Rows per page:</span>
            <select
                className={selectClass}
                value={value}
                onChange={e => onChange(Number(e.target.value))}
            >
                <option value={10}>10</option>
                <option value={15}>15</option>
                <option value={20}>20</option>
                <option value={25}>25</option>
                <option value={30}>30</option>
            </select>
        </span>
    );
}
