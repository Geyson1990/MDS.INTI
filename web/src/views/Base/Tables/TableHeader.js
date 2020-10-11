import React from 'react';
import TableCell from './TableCell';

const TableHeader = ({ headers }) => {
    return (
        <thead key={1}>
            <tr>
                {
                    headers.map((d, i) => <TableCell key={i} data={d} index={i} header={true} />)
                }
            </tr>
        </thead>
    )
}

export default TableHeader;