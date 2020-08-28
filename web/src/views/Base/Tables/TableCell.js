import React from 'react';
const TableCell = ({ data, index, header }) => {
    return (
        header ?
            (<th key={index}>{data.text}</th>) :
            (<td key={index}>{data.text}</td>)
    )
}

export default TableCell;