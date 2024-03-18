export const sanitizeFilter: <T>(dirtyFilter: T) => T = (dirtyFilters) => {
  return (
    dirtyFilters &&
    (Object.keys(dirtyFilters).reduce(
      (acc, key) =>
        !!dirtyFilters[key] ? { ...acc, [key]: dirtyFilters[key] } : acc,
      {}
    ) as typeof dirtyFilters)
  );
};
