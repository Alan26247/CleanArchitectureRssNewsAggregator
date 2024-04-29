import { useEffect, useState } from "react";
import styles from "./PaginationUI.module.css";

const PaginationUI = (data: {
    pageNumber: number,
    countPages: number,
    updatePage: (id: number) => void
}) => {

    let [pageNumber, setPageNumber] = useState<number>(data.pageNumber);
    let [countPages, setCountPages] = useState<number>(data.countPages);
    let [pages, setPages] = useState<number[]>(new Array<number>());

    useEffect(() => {
        setParameters(data.pageNumber, data.countPages);
    }, [data.pageNumber, data.countPages]);

    const setParameters = (currentPageNumber: number, currentCountPages: number) => {
        pages = new Array<number>();
        pageNumber = currentPageNumber;
        countPages = currentCountPages;

        if (currentPageNumber < 1) countPages = 1;

        if (currentPageNumber < 1) pageNumber = 1;
        else if (currentPageNumber > currentCountPages) pageNumber = countPages;

        pages = new Array<number>();

        for (let i = 1; i <= countPages; i++) pages.push(i);

        setPageNumber(pageNumber);
        setCountPages(countPages);
        setPages(pages);
    }

    const pressDown = () => {
        pageNumber--;

        if (pageNumber < 1) pageNumber = 1;

        data.updatePage(pageNumber);

        setPageNumber(pageNumber);
    }

    const selectPage = (currentPageNumber: number) => {
        pageNumber = currentPageNumber;

        if (pageNumber < 1) pageNumber = 1;
        else if (pageNumber > countPages) pageNumber = countPages;

        data.updatePage(pageNumber);

        setPageNumber(pageNumber);
    }

    const pressUp = () => {
        pageNumber++;

        if (pageNumber > countPages) pageNumber = countPages;

        data.updatePage(pageNumber);

        setPageNumber(pageNumber);
    }

    return (
        <div className={styles.content}>
            <span
                className={`${styles.arrow} ${pageNumber === 1 ? styles.disabled : ''}`}
                onClick={pressDown} >
                {'<'}
            </span>

            {pages.map((item: number) => (
                <span
                    key={item}
                    className={`${item === pageNumber && styles.isSelected} ${styles.page_button}`}
                    onClick={() => selectPage(item)}>
                    {item}
                </span>
            ))}

            <span className={`${styles.arrow} ${pageNumber !== countPages ? styles.disabled : ''}`} onClick={pressUp}>
                {'>'}
            </span>
        </div>
    );
}

export default PaginationUI;