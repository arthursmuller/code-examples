import { useState, useEffect } from 'react';

import { useMutationObserver } from '../../hooks/use-mutation-observer';

// import waitForElementTransition from 'wait-for-element-transition';

/*!
 * Wait-for-element-transition v3.3.0
 * https://github.com/mkay581/wait-for-element-transition#readme
 *
 * Copyright (c) 2021 Mark Kennedy
 * Licensed under the MIT license
 */

/**
 * Takes a value and separates the number and unit into a key/value map.
 */
const getCssPropUnitMap = (v) => {
  v = v.trim();
  const numParts = v.match('[0-9.]+');
  let unit = 'ms';
  let num;
  const numString = numParts ? numParts[0] : '';
  if (numString) {
    unit = v.split(numString)[1];
    num = Number(numString);
  }
  return {
    num,
    unit,
  };
};
/**
 * Converts a css timing unit value into milliseconds.
 */
const convertCssTimeValueToMilliseconds = (val) => {
  const map = getCssPropUnitMap(val);
  const num = map ? map.num : undefined;
  if (!num) {
    return '';
  }
  const unit = val.replace(`${num}`, '');
  let value = num;
  if (unit === 's') {
    value = num * 1000;
  }
  return `${value}ms`;
};
/**
 * Gets the time is takes for the element to transition to its show state.
 */
function getTransitionDuration(el) {
  /**
   * Takes a css property name and returns the javascript version of it.
   */
  const getJsPropName = (cssProp) => {
    // convert to camelCase
    return cssProp.replace(/-([a-z])/g, (letter) => {
      return letter[1].toUpperCase();
    });
  };
  /**
   * Gets the computed property of the element.
   */
  const getCssComputedProperty = (prop) => {
    const style = window.getComputedStyle(el);
    return style.getPropertyValue(prop) || el.style[getJsPropName(prop)];
  };
  const delayProp = getCssComputedProperty('transition-delay') || '0ms';
  const durationProp = getCssComputedProperty('transition-duration') || '0ms';
  const times = Array.isArray(durationProp) ? durationProp : [durationProp];
  const delay = Array.isArray(delayProp) ? delayProp : [delayProp];
  let highest = 0;
  let map;
  times.push.apply(times, delay); // account for delay
  // calculate highest number of time
  times.forEach((value) => {
    value.split(',').forEach((v) => {
      v = convertCssTimeValueToMilliseconds(v);
      map = getCssPropUnitMap(v);
      if (map.num && map.num > highest) {
        highest = map.num;
      }
    });
  });
  return highest;
}
/**
 * Builds a transition promise that waits to resolve until the module el's CSS transition is completed (if applicable).
 */
function waitForElementTransition(el) {
  const duration = getTransitionDuration(el);
  return new Promise((resolve) => {
    if (duration > 0) {
      setTimeout(() => {
        resolve(el);
      }, duration);
    } else {
      resolve(el);
    }
  });
}

export const useToasterContainerHeight = (
  elementId = 'chakra-toast-manager-top',
): number => {
  const [element, setElement] = useState(null);

  useEffect(() => {
    const targetElement = document.getElementById(elementId);
    setElement(targetElement);
  }, [elementId]);

  return useMutationObserver<number>({
    initialValue: 0,
    targetNode: element,
    config: { childList: true },
    callback: async ({ mutationList, targetNode }) => {
      if (mutationList.length && mutationList[0].target.childNodes.length) {
        const { childNodes } = mutationList[0].target;

        await waitForElementTransition(
          childNodes[childNodes.length - 1] as HTMLElement,
        );

        return (targetNode as HTMLElement).offsetHeight;
      }
    },
  });
};
