import React from 'react';
import TableCell from './TableCell';

const TableHeader = ({ headers }) => {
    return (
        <thead className="table-row">
            {
                headers.map((d) => <TableCell data={d} />)
            }
        </thead>
    )
}

export default TableHeader;