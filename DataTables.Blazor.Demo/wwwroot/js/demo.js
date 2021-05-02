window.demo = {
    logSummary: (td, cellData, rowData, row, col) => {
        console.log(`(Row ${(row + 1)}, Column ${(col + 1)}) = ${cellData}`);
    },
    renderDate: data => new Date(data).toLocaleString('en-GB')
};
