import React from 'react';
import TableCell from './TableCell';

const TableData = ({ data, meta }) => {
    const headerOrder = meta.map(m => m.key);
    return (
        <tbody>
            {
                data.map((row) => (
                    <tr className="table-row">
                        {
                            data === null ?
                                row.map((_, i) => <TableCell data={row.find(r => r.key === headerOrder[i])} />) :
                                <td>{'Sin resultados'}</td>
                        }
                    </tr>
                ))
            }
        </tbody>
    )
}

export default TableData;