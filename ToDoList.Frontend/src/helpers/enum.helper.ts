export function enumHelper(e: object): any[] {
  const array = Object.values(e);
  return array.splice(array.length / 2, array.length);
}
