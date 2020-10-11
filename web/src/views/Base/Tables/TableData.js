import React from 'react';
import TableCell from './TableCell';

const TableData = ({ data, meta, edit, eliminar }, fnEditar, fnEliminar) => {
    const headerOrder = meta.map(m => m.key);
    return (
        <tbody>
            {
                data.length === 0 ?
                    <tr><td>{'Sin resultados'}</td></tr> :
                    data.map((row) => (
                        <tr>
                            {
                                row.map((_, i) => <TableCell data={row.find(r => r.key === headerOrder[i])} index={i} />)
                            }
                            {edit ? <td><i className="fa fa-trash fa-lg mt-4" onClick={fnEditar} /></td> : <></>}
                            {eliminar ? <td><i className="fa fa-edit fa-lg mt-4" onClick={fnEliminar} /></td> : <></>}
                        </tr>
                    ))
            }
        </tbody>
    )
}

export default TableData;