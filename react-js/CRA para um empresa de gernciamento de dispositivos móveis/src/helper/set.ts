export const toggleElement = (rows: Set<number>, rowId: number) => {
  const newSet = new Set(rows);
  if (newSet.has(rowId)) {
    newSet.delete(rowId);
  } else {
    newSet.add(rowId);
  }
  return newSet;
};
